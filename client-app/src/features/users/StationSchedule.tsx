import { observer } from "mobx-react-lite"
import { useStore } from "../../app/stores/store";
import SpinnerLoading from "../../app/common/SpinnerLoading";
import { useState } from "react";
import StationArrivals from "./components/StationArrivals";
import StationDepartures from "./components/StationDepartures";

export default observer(function StationSchedule() {
  const {stationStore} = useStore();
  const {isLoading, selectedStationSchedule} = stationStore;

  const [arrivals, setArrivals] = useState(true);
  const [departures, setDepartures] = useState(false);

  if (isLoading) {
    return <SpinnerLoading />
  }

  function arrivalsFunction() {
    setArrivals(true);
    setDepartures(false);
  }

  function departuresFunction() {
    setArrivals(false);
    setDepartures(true);
  }

  return (
    <div className="container">
      <div className="mt-5">
        <h1 className="text-center">{selectedStationSchedule?.name}</h1>
        <nav>
          <div className='nav nav-tabs' id='nav-tab' role='tablist'>
            <button
              onClick={arrivalsFunction}
              className='nav-link active'
              id='nav-arrivals-tab'
              data-bs-toggle='tab'
              data-bs-target='#nav-arrivals'
              type='button'
              role='tab'
              aria-controls='nav-arrivals'
              aria-selected='false'>
              Arrivals
            </button>
            <button
              onClick={departuresFunction}
              className='nav-link'
              id='nav-departures-tab'
              data-bs-toggle='tab'
              data-bs-target='#nav-departures'
              type='button'
              role='tab'
              aria-controls='nav-departures'
              aria-selected='true'>
              Departures
            </button>
          </div>
        </nav>
        <div className='tab-content' id='nav-tabContent'>
          <div
						className='tab-pane fade show active'
						id='nav-arrivals'
						role='tabpanel'
						aria-labelledby='nav-arrivals-tab'>
					  {arrivals ? <StationArrivals /> : <></>}
					</div>
					<div className='tab-pane fade' id='nav-departures' role='tabpanel' aria-labelledby='nav-departures-tab'>
						{departures ? <StationDepartures /> : <></>}
					</div>
        </div>
      </div>
    </div>
  );
})