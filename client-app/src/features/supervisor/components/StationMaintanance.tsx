import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/store";
import { useEffect } from "react";
import { Link } from "react-router-dom";

export default observer(function StationMaintanance() {
  const {stationStore} = useStore();
  const {loadStations, deleteStation, stations} = stationStore;

  useEffect(() => {
    loadStations();
  }, [loadStations]);

  return (
    <div className='container mt-5'>
      <Link type="button" className="btn btn-primary" to="/create-station">Add Station</Link>
      <table className="table align-middle mb-0 bg-white">
        <thead className="bg-light">
          <tr>
            <th>Name</th>
            <th>Country</th>
            <th>City</th>
            <th>Street</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
            {Array.from(stations.values()).map((station) => (
              <tr key={station.id}>
                <td>{station.name}</td>
                <td>{station.country}</td>
                <td>{station.city}</td>
                <td>{station.street}</td>
                <td>
                <button className="btn btn-danger" onClick={() => deleteStation(station.id)}>Delete</button>
                </td>
              </tr>
            ))}
        </tbody>
      </table>
    </div>
  );
});