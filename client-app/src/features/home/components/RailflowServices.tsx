import { Link } from "react-router-dom";

export default function RailflowServices() {
  return (
    <div className='container my-5'>
			<div className='row p-4 align-items-center border shadow-lg'>
				<div className='col-lg-7 p-3'>
					<h1 className='display-4 fw-bold'>Join Our Community of Rail Travelers!</h1>
					<p className='lead'>
            Create an account in our application and discover more convenient, 
            faster and more intuitive planning of your railway trips!
					</p>
					<div className='d-grid gap-2 justify-content-md-start mb-4 mb-lg-3'>
							<Link className='btn main-color btn-lg text-white' to='/signup'>
								Sign up
							</Link>
					</div>
				</div>
				<div className='col-lg-4 offset-lg-1 shadow-lg lost-image'></div>
			</div>
		</div>
  );
} 