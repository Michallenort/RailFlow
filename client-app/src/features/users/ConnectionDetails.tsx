import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useStripe } from "@stripe/react-stripe-js";
import agent from "../../app/api/agent";
import { ReservationFormValues } from "../../app/models/reservation";
import { loadStripe } from "@stripe/stripe-js";
import { router } from "../../app/router/Routes";


export default observer(function ConnectionDetails() {
  const {connectionStore, userStore} = useStore();
  const {selectedConnection} = connectionStore;
  const {loggedUser} = userStore;
  
  
  const handleBuyTicket = async () => {
    // const reservation: ReservationFormValues = {
    //   date: selectedConnection?.subConnections[0].schedule.date!,
    //   userId: loggedUser?.id!,
    //   firstScheduleId: selectedConnection?.subConnections[0].schedule.id!,
    //   secondScheduleId: selectedConnection?.subConnections[1].schedule.id! || undefined,
    //   startStopName: selectedConnection?.startStationName!,
    //   endStopName: selectedConnection?.endStationName!,
    //   price: selectedConnection?.price!,
    // };

    router.navigate('/payment');
    
  }

  return (
    <div className="container">
      <div className="row">
        <div className="col-md-6 offset-md-3 border rounded p-4 mt-2 shadow">
          <h2 className="text-center m-4">Connection Details</h2>
          <div className="card">
            <div className="card-header">
              <ul className="list-group list-group-flush">
                <li className="list-group-item">
                  <b>Start Station: </b>
                  {selectedConnection?.startStationName}
                </li>
                <li className="list-group-item">
                  <b>Start time: </b>
                  {selectedConnection?.startHour}
                </li>
                <li className="list-group-item">
                  <b>End station: </b>
                  {selectedConnection?.endStationName}
                </li>
                <li className="list-group-item">
                  <b>End Hour: </b>
                  {selectedConnection?.endHour}
                </li>
                <li className="list-group-item">
                  <b>Price: </b>
                  {selectedConnection?.price}
                </li>
              </ul>
            </div>
            
          </div>
          <button className="btn btn-primary mt-1" onClick={handleBuyTicket}>Buy ticket</button>
        </div>
      </div>
      <div className='container mt-5'>
        {selectedConnection?.subConnections.map((subConnection, index) => (
          <div key={index} className="container mt-2">
            <h3>{subConnection.schedule.route.name}</h3>
            <table className="table align-middle mb-0 bg-white">
              <thead className="bg-light">
                <tr>
                  <th>Station</th>
                  <th>Arrival Hour</th>
                  <th>Departure Hour</th>
                </tr>
              </thead>
              <tbody>
                  {subConnection.stops.map((stop) => (
                    <tr key={stop.id}>
                      <td>{stop.stationName}</td>
                      <td>{stop.arrivalTime}</td>
                      <td>{stop.departureTime}</td>
                    </tr>
                  ))}
              </tbody>
            </table>
          </div>
        ))}
      </div>
    </div>
  )
});