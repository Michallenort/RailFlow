import { makeAutoObservable, runInAction } from 'mobx'
import { Route, RouteFormValues } from '../models/route'
import { Pagination, PagingParams } from '../models/pagination'
import agent from '../api/agent'
import { Station } from '../models/station'
import { Stop } from '../models/stop'
import { store } from './store'

export default class RouteStore {
	routes = new Map<string, Route>()
	selectedRoute?: Route = undefined
	isLoading = false
	pagination: Pagination | null = null
	pagingParams = new PagingParams()
	searchTerm: string | null = null

	constructor() {
		makeAutoObservable(this)
	}

	setPagingParams = (pagingParams: PagingParams) => {
		this.pagingParams = pagingParams
	}

	setSearchTerm = (searchTerm: string | null) => {
		this.searchTerm = searchTerm
	}

	setPagination = (pagination: Pagination) => {
		this.pagination = pagination
	}

	clearRoutes = () => {
		this.routes.clear()
	}

	get axiosParams() {
		const params = new URLSearchParams()

		if (this.searchTerm) {
			params.append('searchTerm', this.searchTerm)
		}

		params.append('page', this.pagingParams.page.toString())
		params.append('pageSize', this.pagingParams.pageSize.toString())
		return params
	}

	private setRoute = (route: Route) => {
		this.routes.set(route.id, route)
	}

	loadRoutes = async () => {
		this.isLoading = true
		this.clearRoutes()

		try {
			const result = await agent.Routes.list(this.axiosParams)
			result.data.items.forEach((route: Route) => {
				this.setRoute(route)
			})
			this.setPagination(result.data.pagination)
			this.isLoading = false
		} catch (error) {
			console.log(error)
			this.isLoading = false
		}
	}

	loadRoute = async (id: string) => {
		store.stopStore.clearStops()
		let route = this.getRoute(id)
		if (route) {
			this.selectedRoute = route
			let stops = await agent.Stops.list(id)
			stops.data.forEach((stop: Stop) => {
				store.stopStore.setStop(stop)
			})
			return route
		} else {
			this.isLoading = true
			try {
				let routeDetails = await agent.Routes.details(id)
				this.setRoute({
					id: routeDetails.data.id,
					name: routeDetails.data.name,
					startStationName: routeDetails.data.startStationName,
					endStationName: routeDetails.data.endStationName,
					trainNumber: routeDetails.data.trainNumber,
          isActive: routeDetails.data.isActive
				})
				routeDetails.data.stops.forEach((stop: Stop) => {
					store.stopStore.setStop(stop)
				})
				runInAction(() => (this.selectedRoute = route))
				this.isLoading = false
				return route
			} catch (error) {
				console.log(error)
				this.isLoading = false
			}
		}
	}

	private getRoute = (id: string) => {
		return this.routes.get(id)
	}

	createRoute = async (route: RouteFormValues) => {
		try {
			const response = await agent.Routes.create(route)
			const newRoute = new Route(route)
			this.setRoute(newRoute)

			return response
		} catch (error) {
			console.log(error)
		}
	}

  updateActive = async (id: string) => {
    this.isLoading = true;
    try {
      const response = await agent.Routes.updateActive(id);
      runInAction(() => {
        if (response.status === 200) {
          let route = this.getRoute(id);
          if (route) {
            route.isActive = !route.isActive;
            this.setRoute(route);
          }
        }
        this.isLoading = false;
      })
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }

	deleteRoute = async (id: string) => {
	  this.isLoading = true;
	  try {
	    const response = await agent.Routes.delete(id);
	    runInAction(() => {
	      if (response.status === 204) {
					this.routes.delete(id);
				}
	      this.isLoading = false;
	    })
	  } catch(error) {
	    console.log(error);
	    this.isLoading = false;
	  }
	}
}
