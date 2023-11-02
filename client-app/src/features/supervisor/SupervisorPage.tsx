import { useState } from "react";
import UserMaintanance from "./components/UserMaintanance";
import TrainMaintanance from "./components/TrainMaintanance";
import StationMaintanance from "./components/StationMaintanance";
import RouteMaintanance from "./components/RouteMaintanance";

export default function SupervisorPage() {

  const [userMaintanance, setUserMaintanance] = useState(true);
  const [trainMaintainance, setTrainMaintainance] = useState(false);
  const [stationMaintainance, setStationMaintainance] = useState(false);
  const [routeMaintainance, setRouteMaintainance] = useState(false);

  function userMaintananceFunction() {
    setUserMaintanance(true);
    setTrainMaintainance(false);
    setStationMaintainance(false);
    setRouteMaintainance(false);
  }

  function trainMaintananceFunction() {
    setUserMaintanance(false);
    setTrainMaintainance(true);
    setStationMaintainance(false);
    setRouteMaintainance(false);
  }

  function stationMaintananceFunction() {
    setUserMaintanance(false);
    setTrainMaintainance(false);
    setStationMaintainance(true);
    setRouteMaintainance(false);
  }

  function routeMaintananceFunction() {
    setUserMaintanance(false);
    setTrainMaintainance(false);
    setStationMaintainance(false);
    setRouteMaintainance(true);
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
              User Maintanance
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
              Train Maintanance
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
              Station Maintanance
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
              Route Maintanance
            </button>
          </div>
        </nav>
        <div className='tab-content' id='nav-tabContent'>
          <div
						className='tab-pane fade show active'
						id='nav-user'
						role='tabpanel'
						aria-labelledby='nav-user-tab'>
					  {userMaintanance ? <UserMaintanance /> : <></>}
					</div>
					<div className='tab-pane fade' id='nav-train' role='tabpanel' aria-labelledby='nav-train-tab'>
						{trainMaintainance ? <TrainMaintanance /> : <></>}
					</div>
					<div className='tab-pane fade' id='nav-station' role='tabpanel' aria-labelledby='nav-station-tab'>
						{stationMaintainance ? <StationMaintanance /> : <></>}
					</div>
          <div className='tab-pane fade' id='nav-route' role='tabpanel' aria-labelledby='nav-route-tab'>
						{routeMaintainance ? <RouteMaintanance /> : <></>}
					</div>
        </div>
      </div>
    </div>
  );
}