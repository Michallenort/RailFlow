export default function Carousel() {
	return (
		<div className='container mt-5' style={{ height: 550 }}>
			<div className='homepage-carousel-title'>
				<h3>Discover Poland with us.</h3>
			</div>
			<div
				id='carouselExampleControls'
				className='carousel carousel-dark slide mt-5 
        d-none d-lg-block'
				data-bs-interval='false'>
				<div className='carousel-inner'>
					<div className='carousel-item active'>
						<div className='row d-flex justify-content-center align-items-center'>
							<div className='col-xs-6 col-sm-6 col-md-4 col-lg-3 mb-3'>
								<div className='text-center'>
									<img
										src={require('../../../Images/warsaw.jpg')}
										width='351'
										height='233'
										alt='warsaw'
									/>
									<h3 className='mt-2'>Warsaw</h3>
								</div>
							</div>
						</div>
					</div>
					<div className='carousel-item'>
						<div className='row d-flex justify-content-center align-items-center'>
							<div className='col-xs-6 col-sm-6 col-md-4 col-lg-3 mb-3'>
								<div className='text-center'>
									<img
										src={require('../../../Images/cracow.jpg')}
										width='351'
										height='233'
										alt='cracow'
									/>
									<h3 className='mt-2'>Cracow</h3>
								</div>
							</div>
						</div>
					</div>
					<div className='carousel-item'>
						<div className='row d-flex justify-content-center align-items-center'>
							<div className='col-xs-6 col-sm-6 col-md-4 col-lg-3 mb-3'>
								<div className='text-center'>
									<img
										src={require('../../../Images/wroclaw.jpg')}
										width='351'
										height='233'
										alt='wroclaw'
									/>
									<h3 className='mt-2'>Wroclaw</h3>
								</div>
							</div>
						</div>
					</div>
					<button
						className='carousel-control-prev'
						type='button'
						data-bs-target='#carouselExampleControls'
						data-bs-slide='prev'>
						<span className='carousel-control-prev-icon' aria-hidden='true'></span>
						<span className='visually-hidden'>Previous</span>
					</button>
					<button
						className='carousel-control-next'
						type='button'
						data-bs-target='#carouselExampleControls'
						data-bs-slide='next'>
						<span className='carousel-control-next-icon' aria-hidden='true'></span>
						<span className='visually-hidden'>Next</span>
					</button>
				</div>
			</div>
		</div>
	)
}
