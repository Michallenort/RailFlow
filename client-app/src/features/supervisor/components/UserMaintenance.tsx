import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/store";
import { useEffect, useState } from "react";
import { Pagination } from "../../../app/common/Pagination";
import { PagingParams } from "../../../app/models/pagination";
import { Link } from "react-router-dom";

export default observer(function UserMaintenance() {
  const {userStore} = useStore();
  const {loadUsers, users, deleteUser, setPagingParams, pagination, isLoading, searchTerm, setSearchTerm} = userStore;
  const [currentPage, setCurrentPage] = useState(1);

  useEffect(() => {
    loadUsers();
  }, [currentPage]);

  const paginate = (pageNumber: number) => {
    setCurrentPage(pageNumber);
    setPagingParams(new PagingParams(pageNumber, 10));
  }

  const searchHandleChange = () => {
    setCurrentPage(1);
    setPagingParams(new PagingParams(1, 10));
    loadUsers();
  }

  return (
    <div className='container mt-5'>
      <Link type="button" className="btn btn-primary" to="/create-user">Add User</Link>
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
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email</th>
            <th>Date of birth</th>
            <th>Nationality</th>
            <th>Role</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
            {Array.from(users.values()).map((user) => (
              <tr key={user.id}>
                <td>{user.firstName}</td>
                <td>{user.lastName}</td>
                <td>{user.email}</td>
                <td>{user.dateOfBirth.toLocaleString()}</td>
                <td>{user.nationality}</td>
                <td>{user.roleName}</td>
                <td>
                <button className="btn btn-danger" onClick={() => deleteUser(user.id)}>Delete</button>
                </td>
              </tr>
            ))}
        </tbody>
      </table>
      <Pagination currentPage={currentPage} totalPages={pagination ? pagination.totalPages : 1} paginate={paginate} />
    </div>
  );
});