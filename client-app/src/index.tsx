import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import App from './app/layout/App'
import { StoreContext, store } from './app/stores/store'
import { RouterProvider } from 'react-router-dom'
import { router } from './app/router/Routes'

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement)
root.render(
	<StoreContext.Provider value={store}>
		<RouterProvider router={router}/>
	</StoreContext.Provider>
)
