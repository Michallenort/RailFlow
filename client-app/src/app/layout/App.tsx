import { Outlet, ScrollRestoration, useLocation } from 'react-router-dom';
import HomePage from '../../features/home/HomePage';
import NavBar from './NavBar';
import './App.css';

function  App() {

  const location = useLocation();


  return (
    <>
      <NavBar /> 
      {location.pathname === '/' ? <HomePage /> : (
        <>
           <Outlet />
        </>
        )}
    </>
  );
}

export default App;
