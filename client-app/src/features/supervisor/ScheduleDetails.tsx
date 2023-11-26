import { observer } from "mobx-react-lite";
import { Link, useParams } from "react-router-dom";
import { useStore } from "../../app/stores/store";
import { assign } from "mobx/dist/internal";

export default observer(function ScheduleDetails() {
  const {id} = useParams();
  const {scheduleStore, assignmentStore} = useStore();
  const {selectedAssignments, deleteAssignment} = assignmentStore;

  return (
    <div className='container mt-5'>
    <Link type="button" className="btn btn-primary" to={`/create-assignment/${id}`}>Add Assignment</Link>
    <table className="table align-middle mb-0 bg-white">
      <thead className="bg-light">
        <tr>
          <th>Email</th>
          <th>Start Hour</th>
          <th>End Hour</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
          {Array.from(selectedAssignments.values()).map((assignment) => (
            <tr key={assignment.id}>
              <td>{assignment.userEmail}</td>
              <td>{assignment.startHour}</td>
              <td>{assignment.endHour}</td>
              <td>
                <button className="btn btn-danger" onClick={() => deleteAssignment(assignment.id)}>Delete</button>
              </td>
            </tr>
          ))}
      </tbody>
    </table>
  </div>
  )
});