import { makeAutoObservable, runInAction } from "mobx"
import { Pagination, PagingParams } from "../models/pagination"
import { Schedule } from "../models/schedule"
import agent from "../api/agent"
import { store } from "./store"
import { Assignment } from "../models/assignment"

export default class ScheduleStore {
  schedules = new Map<string, Schedule>()
	selectedSchedule?: Schedule = undefined
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

	clearSchedules = () => {
		this.schedules.clear()
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

	private setSchedule = (schedule: Schedule) => {
		this.schedules.set(schedule.id, schedule);
	}

	loadSchedules = async () => {
		this.isLoading = true;
		this.clearSchedules();

		try {
			const result = await agent.Schedules.list(this.axiosParams)
			result.data.items.forEach((schedule: Schedule) => {
				this.setSchedule(schedule);
			})
			this.setPagination(result.data.pagination);
			this.isLoading = false;
		} catch (error) {
			console.log(error);
			this.isLoading = false;
		}
	}

	loadSchedule = async (id: string) => {
	  this.clearSchedules();
		let schedule = this.getSchedule(id);
		if (schedule) {
			this.selectedSchedule = schedule;
			let assignments = await agent.Assignments.list(id);
			assignments.data.forEach((assignment: Assignment) => {
			store.assignmentStore.setAssignment(assignment);
			})
			return schedule;
		} else {
			this.isLoading = true
			try {
				let scheduleDetails = await agent.Schedules.details(id);
				this.setSchedule({
					id: scheduleDetails.data.id,
					date: scheduleDetails.data.date,
					route: scheduleDetails.data.route
				})
				scheduleDetails.data.assignments.forEach((assignment: Assignment) => {
					store.assignmentStore.setAssignment(assignment);
				});

				runInAction(() => {
          schedule = this.getSchedule(id);
          this.selectedSchedule = schedule;
        })
				this.isLoading = false
				return schedule;
			} catch (error) {
				console.log(error)
				this.isLoading = false
			}
		}
	}

	private getSchedule = (id: string) => {
		return this.schedules.get(id)
	}

	// createSchedule = async (route: RouteFormValues) => {
	// 	try {
	// 		const response = await agent.Routes.create(route)
	// 		const newRoute = new Route(route)
	// 		this.setRoute(newRoute)

	// 		return response
	// 	} catch (error) {
	// 		console.log(error)
	// 	}
	// }

  // updateSchedule = async (id: string) => {
  //   this.isLoading = true;
  //   try {
  //     const response = await agent.Routes.updateActive(id);
  //     runInAction(() => {
  //       if (response.status === 200) {
  //         let route = this.getRoute(id);
  //         if (route) {
  //           route.isActive = !route.isActive;
  //           this.setRoute(route);
  //         }
  //       }
  //       this.isLoading = false;
  //     })
  //   } catch(error) {
  //     console.log(error);
  //     this.isLoading = false;
  //   }
  // }

	// deleteSchedule = async (id: string) => {
	//   this.isLoading = true;
	//   try {
	//     const response = await agent.Routes.delete(id);
	//     runInAction(() => {
	//       if (response.status === 204) {
	// 				this.routes.delete(id);
	// 			}
	//       this.isLoading = false;
	//     })
	//   } catch(error) {
	//     console.log(error);
	//     this.isLoading = false;
	//   }
	// }
}