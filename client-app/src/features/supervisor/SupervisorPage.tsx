import { useState } from "react";
import UserMaintenance from "./components/UserMaintenance";
import TrainMaintenance from "./components/TrainMaintenance";
import StationMaintenance from "./components/StationMaintenance";
import RouteMaintenance from "./components/RouteMaintenance";

export default function SupervisorPage() {

  const [userMaintenance, setUserMaintenance] = useState(true);
  const [trainMaintenance, setTrainMaintenance] = useState(false);
  const [stationMaintenance, setStationMaintenance] = useState(false);
  const [routeMaintenance, setRouteMaintenance] = useState(false);

  function userMaintananceFunction() {
    setUserMaintenance(true);
    setTrainMaintenance(false);
    setStationMaintenance(false);
    setRouteMaintenance(false);
  }

  function trainMaintananceFunction() {
    setUserMaintenance(false);
    setTrainMaintenance(true);
    setStationMaintenance(false);
    setRouteMaintenance(false);
  }

  function stationMaintananceFunction() {
    setUserMaintenance(false);
    setTrainMaintenance(false);
    setStationMaintenance(true);
    setRouteMaintenance(false);
  }

  function routeMaintananceFunction() {
    setUserMaintenance(false);
    setTrainMaintenance(false);
    setStationMaintenance(false);
    setRouteMaintenance(true);
  }

  return (
    <div className="container">
      <div className="mt-5">
        <h1 className="text-center">Railroad Management</h1>
        <nav>
          <div className='nav nav-tabs' id='nav-tab' role='tablist'>
            <button
              onClick={userMaintananceFunction}
              className='nav-link active'
              id='nav-user-tab'
              data-bs-toggle='tab'
              data-bs-target='#nav-user'
              type='button'
              role='tab'
              aria-controls='nav-user'
              aria-selected='false'>
              User Maintenance
            </button>
            <button
              onClick={trainMaintananceFunction}
              className='nav-link'
              id='nav-train-tab'
              data-bs-toggle='tab'
              data-bs-target='#nav-train'
              type='button'
              role='tab'
              aria-controls='nav-train'
              aria-selected='true'>
              Train Maintenance
            </button>
            <button
              onClick={stationMaintananceFunction}
              className='nav-link'
              id='nav-station-tab'
              data-bs-toggle='tab'
              data-bs-target='#nav-station'
              type='button'
              role='tab'
              aria-controls='nav-station'
              aria-selected='false'>
              Station Maintenance
            </button>
            <button
              onClick={routeMaintananceFunction}
              className='nav-link'
              id='nav-route-tab'
              data-bs-toggle='tab'
              data-bs-target='#nav-route'
              type='button'
              role='tab'
              aria-controls='nav-route'
              aria-selected='false'>
              Route Maintenance
            </button>
          </div>
        </nav>
        <div className='tab-content' id='nav-tabContent'>
          <div
						className='tab-pane fade show active'
						id='nav-user'
						role='tabpanel'
						aria-labelledby='nav-user-tab'>
					  {userMaintenance ? <UserMaintenance /> : <></>}
					</div>
					<div className='tab-pane fade' id='nav-train' role='tabpanel' aria-labelledby='nav-train-tab'>
						{trainMaintenance ? <TrainMaintenance /> : <></>}
					</div>
					<div className='tab-pane fade' id='nav-station' role='tabpanel' aria-labelledby='nav-station-tab'>
						{stationMaintenance ? <StationMaintenance /> : <></>}
					</div>
          <div className='tab-pane fade' id='nav-route' role='tabpanel' aria-labelledby='nav-route-tab'>
						{routeMaintenance ? <RouteMaintenance /> : <></>}
					</div>
        </div>
      </div>
    </div>
  );
}