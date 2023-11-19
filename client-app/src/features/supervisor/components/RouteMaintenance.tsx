import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/store";
import { useEffect, useState } from "react";
import { PagingParams } from "../../../app/models/pagination";
import SpinnerLoading from "../../../app/common/SpinnerLoading";
import { Pagination } from "../../../app/common/Pagination";
import { Link } from "react-router-dom";
import { router } from "../../../app/router/Routes";

export default observer(function RouteMaintenance() {
  const {routeStore} = useStore();
  const {loadRoutes, loadRoute, routes, setPagingParams, pagination, isLoading, searchTerm, setSearchTerm, updateActive, deleteRoute} = routeStore;
  const [currentPage, setCurrentPage] = useState(1);

  useEffect(() => {
    loadRoutes();
  }, [currentPage]);

  const paginate = (pageNumber: number) => {
    setCurrentPage(pageNumber);
    setPagingParams(new PagingParams(pageNumber, 10));
  }

  const searchHandleChange = () => {
    setCurrentPage(1);
    setPagingParams(new PagingParams(1, 10));
    loadRoutes();
  }

  const hanleEdit = (id: string) => {
    loadRoute(id);
    router.navigate(`/route-details/${id}`);
  }

  if (isLoading) {
    return <SpinnerLoading />
  }

  return (
    <div className='container mt-5'>
      <Link type="button" className="btn btn-primary" to="/create-route">Add Route</Link>
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
            <th>Start Station</th>
            <th>End Station</th>
            <th>Train</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
            {Array.from(routes.values()).map((route) => (
              <tr key={route.id}>
                <td>{route.name}</td>
                <td>{route.startStationName}</td>
                <td>{route.endStationName}</td>
                <td>{route.trainNumber}</td>
                <td>
                  <button  className="btn btn-primary mx-1" 
                    onClick={() => updateActive(route.id)}>
                    {route.isActive ? 'Deactivate' : 'Activate'}
                  </button>
                  <button className="btn btn-primary mx-1" onClick={() => hanleEdit(route.id)}>Edit</button>
                  <button className="btn btn-danger" onClick={() => deleteRoute(route.id)}>Delete</button>
                </td>
              </tr>
            ))}
        </tbody>
      </table>
      <Pagination currentPage={currentPage} totalPages={pagination ? pagination.totalPages : 1} paginate={paginate} />
    </div>
  );
});