import { RouteObject, createBrowserRouter } from 'react-router-dom'
import App from '../layout/App'
import SignInForm from '../../features/users/SignInForm';
import SignUpForm from '../../features/users/SignUpForm';

export const routes: RouteObject[] = [
	{
		path: '/',
		element: <App />,
		children: [
			{path: 'signin', element: <SignInForm />},
			{path: 'signup', element: <SignUpForm />}
		]
	}
]

export const router = createBrowserRouter(routes);