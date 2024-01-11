using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;
using Org.BouncyCastle.Crypto.Operators;
using RailFlow.Application.Exceptions;
using Railflow.Core.Repositories;
using Supabase;

namespace RailFlow.Application.Reservations.Commands.Handlers;

internal sealed class GenerateTicketHandler : IRequestHandler<GenerateTicket, string>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRouteRepository _routeRepository;
    private readonly IStopRepository _stopRepository;
    private readonly Client _client;

    public GenerateTicketHandler(IReservationRepository reservationRepository, IRouteRepository routeRepository, 
        IStopRepository stopRepository, Client client)
    {
        _reservationRepository = reservationRepository;
        _routeRepository = routeRepository;
        _stopRepository = stopRepository;
        _client = client;
    }
    
    public async Task<string> Handle(GenerateTicket request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);
        
        if (reservation is null)
        {
            throw new ReservationNotFoundException(request.ReservationId);
        }
        
        MemoryStream stream = new MemoryStream();
        Document document = new Document();
        PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
        
        document.Open();

        Font font = new Font(Font.FontFamily.TIMES_ROMAN, 16, Font.NORMAL);
        
        document.Add(new Paragraph("", font));
        
        int numColumns = 2;
        
        // if (reservation.SecondScheduleId is not null)
        //     numColumns++;
        //
        // if (reservation.TransferStopId is not null)
        //     numColumns++;
        
        PdfPTable table = new PdfPTable(numColumns);
        var columnWidths = Enumerable.Repeat(1f, numColumns).ToArray();
        table.SetWidths(columnWidths);
        
        Font tableFont = new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.NORMAL);
        table.DefaultCell.Phrase = new Phrase { Font = tableFont };

        table.AddCell(new Phrase("Owner", tableFont));
        table.AddCell(new Phrase(reservation.User.Email, tableFont));
        
        table.AddCell(new Phrase("Date", tableFont));
        table.AddCell(new Phrase(reservation.Date.ToString(), tableFont));
        
        table.AddCell(new Phrase("First Schedule", tableFont));
        table.AddCell(new Phrase(reservation.FirstSchedule.Route.Name, tableFont));

        if (reservation.SecondScheduleId is not null)
        {
            table.AddCell(new Phrase("Second Schedule", tableFont));
            var route = await _routeRepository.GetByIdAsync(reservation.SecondSchedule!.RouteId);
            table.AddCell(new Phrase(route!.Name, tableFont));
        }

        table.AddCell(new Phrase("Start Stop", tableFont));
        table.AddCell(new Phrase(reservation.StartStop.Station.Name, tableFont));
        
        table.AddCell(new Phrase("Start Hour", tableFont));
        table.AddCell(new Phrase(reservation.StartHour.ToString(), tableFont));
        
        table.AddCell(new Phrase("End Stop", tableFont));
        table.AddCell(new Phrase(reservation.EndStop.Station.Name, tableFont));
        
        table.AddCell(new Phrase("End Hour", tableFont));
        table.AddCell(new Phrase(reservation.EndHour.ToString(), tableFont));
        
        if (reservation.TransferStopId is not null)
        {
            table.AddCell(new Phrase("Transfer Stop", tableFont));
            var stop = await _stopRepository.GetByIdAsync(reservation.TransferStopId.Value);
            table.AddCell(new Phrase(stop!.Station.Name, tableFont));
        }
        
        table.AddCell(new Phrase("Price", tableFont));
        table.AddCell(new Phrase(reservation.Price.ToString(), tableFont));
        
        table.TotalWidth = 500f;
        table.LockedWidth = true;

        document.Add(table);
        
        document.Close();

        byte[] pdfData = stream.ToArray();
        string pdfName = $"Ticket_{Guid.NewGuid()}.pdf";

        await _client.Storage.From("pdf-storage").Upload(
            stream.ToArray(), pdfName);
        
        var url = _client.Storage.From("pdf-storage").GetPublicUrl(pdfName);

        return url;
    }
}