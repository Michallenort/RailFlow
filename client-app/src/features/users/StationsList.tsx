import { observer } from "mobx-react-lite";
import { Pagination } from "../../app/common/Pagination";
import SpinnerLoading from "../../app/common/SpinnerLoading";
import { PagingParams } from "../../app/models/pagination";
import { useStore } from "../../app/stores/store";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { router } from "../../app/router/Routes";

export default observer(function StationsList() {
  const {stationStore} = useStore();
  const {loadStations, stations, setPagingParams, pagination, isLoading, searchTerm, setSearchTerm, loadStationSchedule} = stationStore;
  const [currentPage, setCurrentPage] = useState(1);

  useEffect(() => {
    loadStations();
  }, [currentPage]);

  const paginate = (pageNumber: number) => {
    setCurrentPage(pageNumber);
    setPagingParams(new PagingParams(pageNumber, 10));
  }

  const searchHandleChange = () => {
    setCurrentPage(1);
    setPagingParams(new PagingParams(1, 10));
    loadStations();
  }

  const handleSelect = (id: string) => {
    loadStationSchedule(id);
    router.navigate(`/stations/${id}`);
  }

  if (isLoading) {
    return <SpinnerLoading />
  }

  return (
    <div className='container mt-5'>
      <div className="row mt-3 mb-2">
        <div className="col-6">
          <div className="d-flex">
            <input 
              className='form-control me-2' 
              type='search' 
              placeholder='Search' 
              aria-labelledby='Search'
              value={searchTerm || ''}
              onChange={e => setSearchTerm(e.target.value)}
            ></input>
            <button className='btn btn-outline-success'
              onClick={() => searchHandleChange()}>
              Search
            </button>
          </div>
        </div>
      </div>
      <table className="table align-middle mb-0 bg-white">
        <thead className="bg-light">
          <tr>
            <th>Name</th>
            <th>Country</th>
            <th>City</th>
            <th>Street</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
            {Array.from(stations.values()).map((station) => (
              <tr key={station.id}>
                <td>{station.name}</td>
                <td>{station.country}</td>
                <td>{station.city}</td>
                <td>{station.street}</td>
                <td>
                  <button className="btn btn-primary" onClick={() => handleSelect(station.id)}>View</button>
                </td>
              </tr>
            ))}
        </tbody>
      </table>
      <Pagination currentPage={currentPage} totalPages={pagination ? pagination.totalPages : 1} paginate={paginate} />
    </div>
  );
});