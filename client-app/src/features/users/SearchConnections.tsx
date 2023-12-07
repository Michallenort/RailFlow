import { observer } from 'mobx-react-lite'
import { useState } from 'react'
import { useStore } from '../../app/stores/store'
import { set } from 'mobx'
import { router } from '../../app/router/Routes'

export default observer(function SearchConnections() {
	const { connectionStore } = useStore()
	const { connections, loadConnections, loadConnection } = connectionStore

	const [startStationName, setStartStationName] = useState('')
	const [endStationName, setEndStationName] = useState('')
	const [date, setDate] = useState('')

	const [displayWarning, setDisplayWarning] = useState(false)

	const handleSearch = (e: any) => {
		e.preventDefault()
		if (startStationName === '' || endStationName === '' || date === '') {
			setDisplayWarning(true)
		} else {
			setDisplayWarning(false)
			loadConnections(startStationName, endStationName, date)
		}
	}

	const handleDetails = (id: number) => {
		loadConnection(id);
		router.navigate('/connection-details');
	}

	return (
		<div className='container mt-5'>
			<div className='d-flex justify-content-center'>
				{displayWarning && (
					<div className='col-md-6 alert alert-danger' role='alert'>
						Not all fields are filled!
					</div>
				)}
			</div>
			<div className='d-flex justify-content-center '>
				<div className='card col-md-6'>
					<div className='card-header'>Search Connection</div>
					<div className='card-body'>
						<form method='POST'>
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
									<label htmlFor='date'>Date</label>
									<input
										type='date'
										className='form-control'
										id='date'
										placeholder='Date'
										value={date}
										onChange={e => setDate(e.target.value)}
									/>
								</div>
							</div>
							<div className='row'>
								<div className='mb-3'>
									<button className='col-md-3 btn btn-primary' onClick={e => handleSearch(e)}>
										Search
									</button>
								</div>
							</div>
						</form>
					</div>
				</div>
			</div>
			<div className='d-flex justify-content-center'>
				{connections.length > 0 ? (
					<table className='table align-middle mb-0 bg-white'>
						<thead className='bg-light'>
							<tr>
								<th>Start Station</th>
								<th>Start Hour</th>
								<th>End Station</th>
								<th>End Hour</th>
								<th>Price</th>
								<th>Actions</th>
							</tr>
						</thead>
						<tbody>
							{connections.map((connection, index) => (
								<tr key={String(index)}>
									<td>{connection.startStationName}</td>
									<td>{connection.startHour}</td>
									<td>{connection.endStationName}</td>
									<td>{connection.endHour}</td>
									<td>{connection.price}</td>
									<td>
										<button className='btn btn-primary' onClick={() => handleDetails(index)}>Details</button>
									</td>
								</tr>
							))}
						</tbody>
					</table>
				) : (
					<div></div>
				)}
			</div>
		</div>
	)
})
