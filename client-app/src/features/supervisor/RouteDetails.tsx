import { observer } from "mobx-react-lite";
import { Link, useParams } from "react-router-dom";
import { useStore } from "../../app/stores/store";

export default observer(function RouteDetails() {
  const {id} = useParams();
  const {routeStore, stopStore} = useStore();
  const {selectedStops, deleteStop} = stopStore;

  return (
    <div className='container mt-5'>
      <h1>Route Details</h1>
    <Link type="button" className="btn btn-primary" to={`/create-stop/${id}`}>Add Stop</Link>
    <table className="table align-middle mb-0 bg-white">
      <thead className="bg-light">
        <tr>
          <th>Station</th>
          <th>Arrival Hour</th>
          <th>Departure Hour</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
          {Array.from(selectedStops.values()).map((stop) => (
            <tr key={stop.id}>
              <td>{stop.stationName}</td>
              <td>{stop.arrivalTime}</td>
              <td>{stop.departureTime}</td>
              <td>
                <button className="btn btn-danger" onClick={() => deleteStop(stop.id)}>Delete</button>
              </td>
            </tr>
          ))}
      </tbody>
    </table>
  </div>
  )
});