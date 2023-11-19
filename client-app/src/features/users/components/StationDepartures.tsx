import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/store";

export default observer(function StationDepartures() {
  const {stationStore} = useStore();
  const {selectedStationSchedule} = stationStore;

  return (
    <table className="table align-middle mb-0 bg-white">
        <thead className="bg-light">
          <tr>
            <th>Start Station</th>
            <th>Arrival Hour</th>
            <th>Route Name</th>
          </tr>
        </thead>
        <tbody>
            {selectedStationSchedule?.stopSchedules.map((stopSchedule) => (
              <tr key={stopSchedule.id}>
                <td>{stopSchedule.route.endStationName}</td>
                <td>{stopSchedule.departureTime}</td>
                <td>{stopSchedule.route.name}</td>
              </tr>
            ))}
        </tbody>
      </table>
  )
});