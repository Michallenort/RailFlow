import { observer } from "mobx-react-lite";
import { useState } from "react";
import { TrainFormValues } from "../../app/models/train";
import agent from "../../app/api/agent";
import { router } from "../../app/router/Routes";

export default observer(function CreateTrain() {
  const [number, setNumber] = useState('');
  const [maxSpeed, setMaxSpeed] = useState('');
  const [capacity, setCapacity] = useState('');

  const [displayWarning, setDisplayWarning] = useState(false);
  const [displaySuccess, setDisplaySuccess] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');

  async function handleSubmit(e: any) {
    e.preventDefault();

    const train: TrainFormValues = {
      number: Number(number),
      maxSpeed: maxSpeed ? Number(maxSpeed) : null,
      capacity: Number(capacity)
    }

    const response = await agent.Trains.create(train);

    if (response.status === 200) {
      setNumber('');
      setMaxSpeed('');
      setCapacity('');

      setDisplaySuccess(true);
      setDisplayWarning(false);
      setErrorMessage('');
    } else {
      setDisplaySuccess(false);
      setDisplayWarning(true);
      setErrorMessage(response.data.reason);
    }
  }   

  function handleCancel() {
    router.navigate('/supervisor');
  }

  return (
    <div className='container mt-5 mb-5'>
			<div className='d-flex justify-content-center'>
				{displaySuccess && (
					<div className='col-md-6 alert alert-success' role='alert'>
						Station added successfully!
					</div>
				)}
				{displayWarning && (
					<div className='col-md-6 alert alert-danger' role='alert'>
						{errorMessage}
					</div>
				)}
			</div>
			<div className='d-flex justify-content-center '>
				<div className='card col-md-6'>
					<div className='card-header'>Add a new train</div>
					<div className='card-body'>
						<form method='POST'>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='number'>Number</label>
									<input
										type='number'
                    min='100000'
                    max='999999'
										className='form-control'
                    placeholder='Number'
										id='number'
										value={number}
										onChange={e => setNumber(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='maxSpeed'>Max Speed</label>
									<input
										type='number'
										className='form-control'
										id='maxSpeed'
										placeholder='Max Speed'
										value={maxSpeed}
										onChange={e => setMaxSpeed(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='capacity'>Capacity</label>
									<input
										type='text'
										className='form-control'
										id='capacity'
										placeholder='Capacity'
										value={capacity}
										onChange={e => setCapacity(e.target.value)}
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
})