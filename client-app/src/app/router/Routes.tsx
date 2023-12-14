import { RouteObject, createBrowserRouter } from 'react-router-dom'
import App from '../layout/App'
import SignInForm from '../../features/users/SignInForm';
import SignUpForm from '../../features/users/SignUpForm';
import RequireAdmin from './RequireAdmin';
import SupervisorPage from '../../features/supervisor/SupervisorPage';
import CreateStation from '../../features/supervisor/CreateStation';
import CreateTrain from '../../features/supervisor/CreateTrain';
import CreateUser from '../../features/supervisor/CreateUser';
import CreateRoute from '../../features/supervisor/CreateRoute';
import RouteDetails from '../../features/supervisor/RouteDetails';
import CreateStop from '../../features/supervisor/CreateStop';
import StationsList from '../../features/users/StationsList';
import StationSchedule from '../../features/users/StationSchedule';
import SearchConnections from '../../features/users/SearchConnections';
import ScheduleDetails from '../../features/supervisor/ScheduleDetails';
import CreateAssignment from '../../features/supervisor/CreateAssignment';
import ConnectionDetails from '../../features/users/ConnectionDetails';
import Payment from '../../features/checkout/Payment';
import Completion from '../../features/checkout/Completion';
import RequireEmployee from './RequireEmployee';
import AssignmentsManagement from '../../features/employee/AssignmentsManagement';
import RequireAuth from './RequireAuth';
import ReservationsList from '../../features/loggedUser/ReservationsList';

export const routes: RouteObject[] = [
	{
		path: '/',
		element: <App />,
		children: [
			{path: 'signin', element: <SignInForm />},
			{path: 'signup', element: <SignUpForm />},
			{path: 'stations', element: <StationsList />},
			{path: 'stations/:id', element: <StationSchedule />},
			{path: 'search', element: <SearchConnections />},
			{path: 'connection-details', element: <ConnectionDetails />},
			{path: 'payment', element: <Payment />},
			{path: 'completion', element: <Completion />},
			{element: <RequireAuth />, children: [
				{path: 'reservations', element: <ReservationsList />}
			]},
			{element: <RequireAdmin />, children: [
				{path: 'supervisor', element: <SupervisorPage />},
				{path: 'create-station', element: <CreateStation />},
				{path: 'create-train', element: <CreateTrain />},
				{path: 'create-user', element: <CreateUser />},
				{path: 'create-route', element: <CreateRoute />},
				{path: 'route-details/:id', element: <RouteDetails />},
				{path: 'create-stop/:routeId', element: <CreateStop />},
				{path: 'schedule-details/:id', element: <ScheduleDetails />},
				{path: 'create-assignment/:scheduleId', element: <CreateAssignment />}
			]},
			{element: <RequireEmployee />, children: [
				{path: 'emplouee-assignments', element: <AssignmentsManagement />}
			]}
		]
	}
]

export const router = createBrowserRouter(routes);