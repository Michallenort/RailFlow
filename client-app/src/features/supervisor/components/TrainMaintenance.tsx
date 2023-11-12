import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/store";
import { useEffect, useState } from "react";
import { PagingParams } from "../../../app/models/pagination";
import SpinnerLoading from "../../../app/common/SpinnerLoading";
import { Link } from "react-router-dom";
import { Pagination } from "../../../app/common/Pagination";

export default observer(function TrainMaintenance() {
  const {trainStore} = useStore();
  const {loadTrains, trains, deleteTrain, setPagingParams, pagination, isLoading, searchTerm, setSearchTerm} = trainStore;
  const [currentPage, setCurrentPage] = useState(1);

  useEffect(() => {
    loadTrains();
  }, [currentPage]);

  const paginate = (pageNumber: number) => {
    setCurrentPage(pageNumber);
    setPagingParams(new PagingParams(pageNumber, 10));
  }

  const searchHandleChange = () => {
    setCurrentPage(1);
    setPagingParams(new PagingParams(1, 10));
    loadTrains();
  }

  if (isLoading) {
    return <SpinnerLoading />
  }

  return (
    <div className="container mt-5">
      <Link type="button" className="btn btn-primary" to="/create-train">Add Train</Link>
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
            <th>Number</th>
            <th>Max Speed</th>
            <th>Capacity</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
            {Array.from(trains.values()).map((train) => (
              <tr key={train.id}>
                <td>{train.number}</td>
                <td>{train.maxSpeed || ''}</td>
                <td>{train.capacity}</td>
                <td>
                <button className="btn btn-danger" onClick={() => deleteTrain(train.id)}>Delete</button>
                </td>
              </tr>
            ))}
        </tbody>
      </table>
      <Pagination currentPage={currentPage} totalPages={pagination ? pagination.totalPages : 1} paginate={paginate} />
    </div>

  );
});