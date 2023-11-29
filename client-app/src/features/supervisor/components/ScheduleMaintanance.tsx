import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/store";
import { useEffect, useState } from "react";
import { PagingParams } from "../../../app/models/pagination";
import { router } from "../../../app/router/Routes";
import SpinnerLoading from "../../../app/common/SpinnerLoading";
import { Pagination } from "../../../app/common/Pagination";
import { Link } from "react-router-dom";

export default observer(function ScheduleMaintanance() {
  const {scheduleStore} = useStore();
  const {loadSchedules, loadSchedule, schedules, setPagingParams, pagination, isLoading, searchTerm, setSearchTerm} = scheduleStore;
  const [currentPage, setCurrentPage] = useState(1);

  useEffect(() => {
    loadSchedules();
  }, [currentPage]);

  const paginate = (pageNumber: number) => {
    setCurrentPage(pageNumber);
    setPagingParams(new PagingParams(pageNumber, 10));
  }

  const searchHandleChange = () => {
    setCurrentPage(1);
    setPagingParams(new PagingParams(1, 10));
    loadSchedules();
  }

  const hanleView = (id: string) => {
    loadSchedule(id);
    router.navigate(`/schedule-details/${id}`);
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
            <th>Date</th>
            <th>Start Station</th>
            <th>End Station</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
            {Array.from(schedules.values()).map((schedule) => (
              <tr key={schedule.id}>
                <td>{schedule.route.name}</td>
                <td>{schedule.date.toLocaleString()}</td>
                <td>{schedule.route.startStationName}</td>
                <td>{schedule.route.endStationName}</td>
                <td>
                  <button className="btn btn-primary mx-1" onClick={() => hanleView(schedule.id)}>View</button>
                </td>
              </tr>
            ))}
        </tbody>
      </table>
      <Pagination currentPage={currentPage} totalPages={pagination ? pagination.totalPages : 1} paginate={paginate} />
    </div>
  );
});