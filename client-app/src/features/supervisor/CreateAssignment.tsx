import { observer } from "mobx-react-lite";
import { useParams } from "react-router-dom";
import { useStore } from "../../app/stores/store";
import { useState } from "react";
import { AssignmentFormValues } from "../../app/models/assignment";
import { router } from "../../app/router/Routes";

export default observer(function CreateAssignment() {
  const {scheduleId} = useParams();

  const {assignmentStore} = useStore();
  const {createAssignment} = assignmentStore;

  const [userEmail, setUserEmail] = useState('');
  const [startHour, setStartHour] = useState('');
  const [endHour, setEndHour] = useState('');

  const [displayWarning, setDisplayWarning] = useState(false);
  const [displaySuccess, setDisplaySuccess] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');

  async function handleSubmit(e: any) {
    e.preventDefault();

    const assignment: AssignmentFormValues = {
      userEmail: userEmail,
      startHour: startHour.concat(':00'),
      endHour: endHour.concat(':00'),
      scheduleId: scheduleId!
    }

    createAssignment(assignment).then(response => {
			if (response?.status === 200) {
				setUserEmail('');
				setStartHour('');
				setEndHour('');
	
				setDisplaySuccess(true);
				setDisplayWarning(false);
				setErrorMessage('');
			} else {
				setDisplaySuccess(false);
				setDisplayWarning(true);
				setErrorMessage(response?.data.reason);
			}
		}).catch(error => {
			setDisplaySuccess(false);
			setDisplayWarning(true);
			setErrorMessage(error.data.reason);
		});
  } 

  function handleCancel() {
    router.navigate(`/schedule-details/${scheduleId}`);
  }
  
  return (
    <div className='container mt-5 mb-5'>
			<div className='d-flex justify-content-center'>
				{displaySuccess && (
					<div className='col-md-6 alert alert-success' role='alert'>
						Assignment added successfully!
					</div>
				)}
				{displayWarning && (
					<div className='col-md-6 alert alert-danger' role='alert'>
						{errorMessage || 'Something went wrong!'}
					</div>
				)}
			</div>
			<div className='d-flex justify-content-center '>
				<div className='card col-md-6'>
					<div className='card-header'>Add a new assignment</div>
					<div className='card-body'>
						<form method='POST'>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='userEmail'>User email</label>
									<input
										type='email'
										className='form-control'
                    placeholder='User email'
										id='userEmail'
										value={userEmail}
										onChange={e => setUserEmail(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='startTime'>Start Time</label>
									<input
										type='time'
										className='form-control'
										id='stratTime'
										placeholder='Start Time'
										value={startHour}
										onChange={e => setStartHour(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='endTime'>End Time</label>
									<input
										type='time'
										className='form-control'
										id='endTime'
										placeholder='Departure Time'
										value={endHour}
										onChange={e => setEndHour(e.target.value)}
									/>
								</div>
							</div>
              <div className='row'>
								<div className='mb-3'>
									<button className='col-md-3 btn btn-primary' onClick={e => handleSubmit(e)}>
										Submit
									</button>
                  <button className="col-md-3 mx-2 btn btn-outline-primary" onClick={handleCancel}>
                    Cancel
                  </button>
								</div>
							</div>
						</form>
					</div>
				</div>
			</div>
		</div>
  )
});