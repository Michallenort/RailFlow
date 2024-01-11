import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useEffect, useState } from "react";
import { fileURLToPath } from "url";

export default observer(function ReservationsList() {
  const {reservationStore} = useStore();
  const {reservations, loadReservations, cancelReservation, generateTicket} = reservationStore;
  useEffect(() => {
    loadReservations();
  }, []);

  const handleCancellation = (id: string) => {
    cancelReservation(id);
  }

  const isActive = (date: Date) => {
    const today = new Date();
    return today > new Date(date);
  }

  const downloadTicket = async (id: string) => {
    let ticketUrl = await generateTicket(id);
    const link = document.createElement('a');
    link.href = ticketUrl;
    link.setAttribute(
      'download',
      `Ticket.pdf`,
    );
    document.body.appendChild(link);

    link.click();

    link.parentNode?.removeChild(link);
  }

  return (
    <div className='container mt-5'>
    <h1 className="text-center">My Reservations</h1>
    <table className="table align-middle mt-2 mb-0 bg-white">
      <thead className="bg-light">
        <tr>
          <th>Date</th>
          <th>Start Station</th>
          <th>End Station</th>
          <th>Price</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
          {Array.from(reservations.values()).map((reservation) => (
            <tr key={reservation.id}>
              <td>{reservation.date.toLocaleString()}</td>
              <td>{reservation.startStopName}</td>
              <td>{reservation.endStopName}</td>
              <td>{reservation.price}</td>
              <td>
                {}
                <button disabled={isActive(reservation.date)} className="btn btn-danger" onClick={() => cancelReservation(reservation.id)}>Cancel</button>
                <button className="mx-2 btn btn-primary" onClick={() => downloadTicket(reservation.id)}>Get Ticket</button>
              </td>
            </tr>
          ))}
      </tbody>
    </table>
  </div>
  );
})