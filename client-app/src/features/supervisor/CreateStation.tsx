import { observer } from 'mobx-react-lite'
import { useState } from 'react'
import { Station, StationFormValues } from '../../app/models/station'
import agent from '../../app/api/agent'
import { set } from 'mobx'

export default observer(function CreateStation() {
	const [name, setName] = useState('')
	const [country, setCountry] = useState('')
	const [city, setCity] = useState('')
	const [street, setStreet] = useState('')

	const [displayWarning, setDisplayWarning] = useState(false)
	const [displaySuccess, setDisplaySuccess] = useState(false)
	const [errorMessage, setErrorMessage] = useState('')

	async function handleSubmit(e: any) {
		e.preventDefault()

		const station: StationFormValues = {
			name: name,
			country: country,
			city: city,
			street: street,
		}

		const response = await agent.Stations.create(station)

		if (response.status === 200) {
			setName('')
			setCountry('')
			setCity('')
			setStreet('')

			setDisplaySuccess(true)
			setDisplayWarning(false)
			setErrorMessage('')
		} else {
			setDisplaySuccess(false)
			setDisplayWarning(true)
			setErrorMessage(response.data.reason)
		}
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
						Something went wrong!
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
									<label htmlFor='country'>Country</label>
									<input
										type='text'
										className='form-control'
										id='country'
										placeholder='Country'
										value={country}
										onChange={e => setCountry(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='city'>City</label>
									<input
										type='text'
										className='form-control'
										id='city'
										placeholder='City'
										value={city}
										onChange={e => setCity(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<label htmlFor='street'>Street</label>
									<input
										type='text'
										className='form-control'
										id='street'
										placeholder='Street'
										value={street}
										onChange={e => setStreet(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<button className='col-md-3 btn btn-primary' onClick={e => handleSubmit(e)}>
										Submit
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
