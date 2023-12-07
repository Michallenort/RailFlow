import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useStore } from "../stores/store";

export default function RequireEmployee() {
  const {userStore: {isEmployee}} = useStore();
  const location = useLocation();

  if (!isEmployee) {
    return <Navigate to='/' state={{from: location}}/>
  }

  return <Outlet />
}