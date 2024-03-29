import axios, { AxiosError, AxiosResponse } from "axios";
import { CreateUserFormValues, SignInFormValues, SignUpFormValues, User } from "../models/user";
import { store } from "../stores/store";
import { router } from "../router/Routes";
import { toast } from "react-toastify";
import { Station, StationFormValues, StationSchedule } from "../models/station";
import { PaginatedResult } from "../models/pagination";
import { Train, TrainFormValues } from "../models/train";
import { Route, RouteDetails, RouteFormValues } from "../models/route";
import { Stop, StopFormValues } from "../models/stop";
import { Schedule, ScheduleDetails } from "../models/schedule";
import EmployeeAssignment, { Assignment, AssignmentFormValues } from "../models/assignment";
import { Connection } from "../models/connection";
import { Reservation, ReservationFormValues } from "../models/reservation";
import { Config, PaymentIntent } from "../models/checkout";

axios.defaults.baseURL = "http://localhost:44363";

const responseBody = (response: AxiosResponse) => response;

axios.interceptors.request.use(config => {
  const token = store.tokenStore.token;
  if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;
  return config;
})

axios.interceptors.response.use(async response => {
  const {page, pageSize, totalCount, totalPages} = response.data;

  if (page && pageSize && totalCount && totalPages) {
    response.data = new PaginatedResult(response.data.items, {page, pageSize, totalCount, totalPages});
    return response as AxiosResponse<PaginatedResult<any>>;
  }

  return response;
}, (error: AxiosError) => {
  const { data, status, config } = error.response as AxiosResponse;
  return error.response;
})

const requests = {
  getPage: <T>(url: string, params: URLSearchParams) => axios.get<PaginatedResult<T>>(url, { params }).then(responseBody),
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody)
}

const Users = {
  signin: (user: SignInFormValues) => requests.post('/User/sign-in', user),
  signup: (user: SignUpFormValues) => requests.post('/User/sign-up', user),
  accountdetails: () => requests.get('/User/account-details'),
  list: (params: URLSearchParams) => requests.getPage<PaginatedResult<User[]>>('/User', params).then(responseBody),
  create: (user: CreateUserFormValues) => requests.post('/User/create-user', user),
  delete: (id: string) => requests.delete(`/User/${id}`)
}

const Stations = {
  list: (params: URLSearchParams) => requests.getPage<PaginatedResult<Station[]>>('/Station', params).then(responseBody),
  details: (id: string) => requests.get(`/Station/${id}`),
  scheduleDetails: (id: string) => requests.get<StationSchedule>(`/Station/schedule/${id}`),
  create: (station: StationFormValues) => requests.post('/Station', station),
  delete: (id: string) => requests.delete(`/Station/${id}`)
}

const Trains = {
  list: (params: URLSearchParams) => requests.getPage<PaginatedResult<Train[]>>('/Train', params).then(responseBody),
  details: (id: string) => requests.get(`/Train/${id}`),
  create: (train: TrainFormValues) => requests.post('/Train', train),
  delete: (id: string) => requests.delete(`/Train/${id}`)
}

const Routes = {
  list: (params: URLSearchParams) => requests.getPage<PaginatedResult<Route[]>>('/Route', params).then(responseBody),
  details: (id: string) => requests.get<RouteDetails>(`/Route/${id}`),
  create: (route: RouteFormValues) => requests.post('/Route', route),
  updateActive: (id: string) => axios.put(`/Route/update-active/${id}`).then(responseBody),
  delete: (id: string) => requests.delete(`/Route/${id}`)
}

const Stops = {
  list: (routeId: string) => requests.get<Stop[]>(`/Stop/${routeId}`),
  create: (stop: StopFormValues) => requests.post('/Stop', stop),
  delete: (id: string) => requests.delete(`/Stop/${id}`)
}

const Schedules = {
  list: (params: URLSearchParams) => requests.getPage<PaginatedResult<Schedule[]>>
  ('/Schedule', params).then(responseBody),
  details: (id: string) => requests.get<ScheduleDetails>(`/Schedule/${id}`),
}

const Assignments = {
  list: (scheduleId: string) => requests.get<Assignment[]>(`/Assignment/${scheduleId}`),
  employeeList: (userId: string) => requests.get<EmployeeAssignment[]>(`/Assignment/employee/${userId}`),
  create: (assignment: AssignmentFormValues) => requests.post('/Assignment', assignment),
  delete: (id: string) => requests.delete(`/Assignment/${id}`)
}

const Connections = {
  list: (startStation: string, endStation: string, date: string) => requests.get<Connection[]>(
    `/Connection?startStation=${startStation}&endStation=${endStation}&date=${date}`) 
}

const Reservations = {
  list: (userId: string) => requests.get<Reservation[]>(`/Reservation/${userId}`),
  create: (reservation: ReservationFormValues) => requests.post('/Reservation', reservation),
  cancel: (id: string) => requests.delete(`/Reservation/${id}`),
  generateTicket: (id: string) => axios.post(`/Reservation/generate-pdf/${id}`)
}

const Checkouts = {
  config: () => requests.get<Config>(`/Checkout/config`),
  createPaymentIntent: (intent: PaymentIntent) => requests.post("/Checkout/create-payment-intent", intent)
}

const agent = {
  Users,
  Stations,
  Trains,
  Routes,
  Stops,
  Schedules,
  Assignments,
  Connections,
  Checkouts,
  Reservations
}

export default agent;