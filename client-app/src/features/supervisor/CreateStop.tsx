import { observer } from "mobx-react-lite";
import { useState } from "react";
import { router } from "../../app/router/Routes";
import { useStore } from "../../app/stores/store";
import { StopFormValues } from "../../app/models/stop";
import { useParams } from "react-router-dom";

export default observer(function CreateStop() {
  const {routeId} = useParams();

  const {stopStore} = useStore();
  const {createStop} = stopStore;

  const [stationName, setStationName] = useState('');
  const [arrivalTime, setArrivalTime] = useState('');
  const [departureTime, setDepartureTime] = useState('');

  const [displayWarning, setDisplayWarning] = useState(false);
  const [displaySuccess, setDisplaySuccess] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');

  async function handleSubmit(e: any) {
    e.preventDefault();

    const stop: StopFormValues = {
      stationName: stationName,
      arrivalTime: arrivalTime.concat(':00'),
      departureTime: departureTime.concat(':00'),
      routeId: routeId!
    }

    createStop(stop).then(response => {
			if (response?.status === 200) {
				setStationName('');
				setArrivalTime('');
				setDepartureTime('');
	
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
    router.navigate(`/route-details/${routeId}`);
  }

  return (
    <div className='container mt-5 mb-5'>
			<div className='d-flex justify-content-center'>
				{displaySuccess && (
					<div className='col-md-6 alert alert-success' role='alert'>
						Stop added successfully!
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
					<div className='card-header'>Add a new stop</div>
					<div className='card-body'>
						<form method='POST'>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='stationName'>Station Name</label>
									<input
										type='text'
										className='form-control'
                    placeholder='Station Name'
										id='stationName'
										value={stationName}
										onChange={e => setStationName(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='arrivalTime'>Arrival Time</label>
									<input
										type='time'
										className='form-control'
										id='arrivalTime'
										placeholder='Arrival Time'
										value={arrivalTime}
										onChange={e => setArrivalTime(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='departureTime'>Departure Time</label>
									<input
										type='time'
										className='form-control'
										id='departureTime'
										placeholder='Departure Time'
										value={departureTime}
										onChange={e => setDepartureTime(e.target.value)}
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
  );
})