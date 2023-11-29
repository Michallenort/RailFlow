import { Outlet, ScrollRestoration, useLocation } from 'react-router-dom';
import HomePage from '../../features/home/HomePage';
import NavBar from './NavBar';
import './App.css';
import { observer } from 'mobx-react-lite';
import Footer from './Footer';

function App() {

  const location = useLocation();


  return (
    <>
      <NavBar /> 
      <div className='d-flex flex-column min-vh-100'>
        {location.pathname === '/' ? <HomePage /> : (
          <>
            <Outlet />
          </>
        )}
      </div>
      <Footer />
    </>
  );
}

export default observer(App);
