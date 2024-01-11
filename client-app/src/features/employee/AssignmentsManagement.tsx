import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useEffect } from "react";

export default observer(function AssignmentsManagement() {
  const {assignmentStore} = useStore();
  const {employeeAssignments, loadEmployeeAssignments} = assignmentStore;

  useEffect(() => {
    loadEmployeeAssignments();
  }, [])

  return (
    <div className='container mt-5'>
      <h1>Work Schedule</h1>
      <table className="table align-middle mb-0 bg-white">
      <thead className="bg-light">
        <tr>
          <th>Route Name</th>
          <th>Date</th>
          <th>Start Hour</th>
          <th>End Hour</th>
        </tr>
      </thead>
      <tbody>
          {Array.from(employeeAssignments.values()).map((assignment) => (
            <tr key={assignment.id}>
              <td>{assignment.routeName}</td>
              <td>{assignment.date.toLocaleString()}</td>
              <td>{assignment.startHour}</td>
              <td>{assignment.endHour}</td>
            </tr>
          ))}
      </tbody>
    </table>
    </div>
  )
});