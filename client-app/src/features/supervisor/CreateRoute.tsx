import { observer } from 'mobx-react-lite'
import { useStore } from '../../app/stores/store'
import { useState } from 'react'
import { router } from '../../app/router/Routes'
import { RouteFormValues } from '../../app/models/route'

export default observer(function CreateRoute() {
	const { routeStore } = useStore()
	const { createRoute: createStation } = routeStore

	const [name, setName] = useState('')
	const [startStationName, setStartStationName] = useState('')
	const [endStationName, setEndStationName] = useState('')
	const [trainNumber, setTrainNumber] = useState('')

	const [displayWarning, setDisplayWarning] = useState(false)
	const [displaySuccess, setDisplaySuccess] = useState(false)
	const [errorMessage, setErrorMessage] = useState('')

	async function handleSubmit(e: any) {
		e.preventDefault()

		const station: RouteFormValues = {
			name: name,
			startStationName: startStationName,
			endStationName: endStationName,
			trainNumber: Number(trainNumber),
		}

		createStation(station)
			.then(response => {
				if (response?.status === 200) {
					setName('')
					setStartStationName('')
					setEndStationName('')
					setTrainNumber('')

					setDisplaySuccess(true)
					setDisplayWarning(false)
					setErrorMessage('')
				} else {
					setDisplaySuccess(false)
					setDisplayWarning(true)
					setErrorMessage(response?.data.reason)
				}
			})
			.catch(error => {
				setDisplaySuccess(false)
				setDisplayWarning(true)
				setErrorMessage(error.data.reason)
			})
	}

	function handleCancel() {
		router.navigate('/supervisor')
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
						{errorMessage || 'Something went wrong!'}
					</div>
				)}
			</div>
			<div className='d-flex justify-content-center '>
				<div className='card col-md-6'>
					<div className='card-header'>Add a new station</div>
					<div className='card-body'>
						<form method='POST'>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='name'>Name</label>
									<input
										type='text'
										className='form-control'
										id='name'
										placeholder='Name'
										value={name}
										onChange={e => setName(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='startStationName'>Start Station</label>
									<input
										type='text'
										className='form-control'
										id='startStationName'
										placeholder='Start Station'
										value={startStationName}
										onChange={e => setStartStationName(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='endStationName'>End Station</label>
									<input
										type='text'
										className='form-control'
										id='endStationName'
										placeholder='End Station'
										value={endStationName}
										onChange={e => setEndStationName(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='trainNumber'>Train</label>
									<input
										type='number'
										min='100000'
										max='999999'
										className='form-control'
										id='trainNumber'
										placeholder='Train'
										value={trainNumber}
										onChange={e => setTrainNumber(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<button className='col-md-3 btn btn-primary' onClick={e => handleSubmit(e)}>
										Submit
									</button>
									<button className='col-md-3 mx-2 btn btn-outline-primary' onClick={handleCancel}>
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
