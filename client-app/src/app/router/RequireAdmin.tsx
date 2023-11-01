import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useStore } from "../stores/store";

export default function RequireAdmin() {
  const {userStore: {isAdmin}} = useStore();
  const location = useLocation();

  if (!isAdmin) {
    return <Navigate to='/' state={{from: location}}/>
  }

  return <Outlet />
}