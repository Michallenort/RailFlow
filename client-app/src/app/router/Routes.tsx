import { RouteObject, createBrowserRouter } from 'react-router-dom'
import App from '../layout/App'
import SignInForm from '../../features/users/SignInForm';
import SignUpForm from '../../features/users/SignUpForm';
import RequireAdmin from './RequireAdmin';
import SupervisorPage from '../../features/supervisor/SupervisorPage';
import CreateStation from '../../features/supervisor/CreateStation';
import CreateTrain from '../../features/supervisor/CreateTrain';
import CreateUser from '../../features/supervisor/CreateUser';

export const routes: RouteObject[] = [
	{
		path: '/',
		element: <App />,
		children: [
			{path: 'signin', element: <SignInForm />},
			{path: 'signup', element: <SignUpForm />},
			{element: <RequireAdmin />, children: [
				{path: 'supervisor', element: <SupervisorPage />},
				{path: 'create-station', element: <CreateStation />},
				{path: 'create-train', element: <CreateTrain />},
				{path: 'create-user', element: <CreateUser />}
			]}
		]
	}
]

export const router = createBrowserRouter(routes);