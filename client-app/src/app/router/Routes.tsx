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
import ScheduleDetails from '../../features/supervisor/ScheduleDetails';
import CreateAssignment from '../../features/supervisor/CreateAssignment';

export const routes: RouteObject[] = [
	{
		path: '/',
		element: <App />,
		children: [
			{path: 'signin', element: <SignInForm />},
			{path: 'signup', element: <SignUpForm />},
			{path: 'stations', element: <StationsList />},
			{path: 'stations/:id', element: <StationSchedule />},
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
			]}
		]
	}
]

export const router = createBrowserRouter(routes);