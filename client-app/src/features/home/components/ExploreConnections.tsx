import { Link } from "react-router-dom";

export default function ExploreConnections() {
  return (
    <div className="p-5 mb-4 bg-dark header">
      <div className="constainer-fluid py-5 text-white
        d-flex justify-content-center align-items-center">
        <div>
          <h1 className="display-5 fw-bold">Find your next adventure</h1>
          <p className="col-md-8 fs-4">Where would you like to go next?</p>
          <Link type="button" className="btn main-color btn-lg text-white" to="/search">
            Explore Connections
          </Link>
        </div>
      </div>
    </div>
  );
}