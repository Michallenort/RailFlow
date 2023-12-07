import { makeAutoObservable, runInAction } from "mobx";
import EmployeeAssignment, { Assignment, AssignmentFormValues } from "../models/assignment";
import { Pagination, PagingParams } from "../models/pagination";
import agent from "../api/agent";
import { store } from "./store";

export default class AssignmentStore {
  selectedAssignments = new Map<string, Assignment>();
  employeeAssignments = new Map<string, EmployeeAssignment>();
  isLoading = false;
  pagination: Pagination | null = null;
  pagingParams = new PagingParams();
  searchTerm: string | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  setPagingParams = (pagingParams: PagingParams) => {
    this.pagingParams = pagingParams;
  }

  setSearchTerm = (searchTerm: string | null) => {
    this.searchTerm = searchTerm;
  }

  setPagination = (pagination: Pagination) => {
    this.pagination = pagination;
  }

  clearAssignments = () => {
    this.selectedAssignments.clear();
  }

  clearEmployeeAssignments = () => {
    this.employeeAssignments.clear();
  }

  get axiosParams() {
    const params = new URLSearchParams();

    if (this.searchTerm) {
      params.append('searchTerm', this.searchTerm);
    }

    params.append('page', this.pagingParams.page.toString());
    params.append('pageSize', this.pagingParams.pageSize.toString());
    return params;
  }

  setAssignment = (assignment: Assignment) => {
    this.selectedAssignments.set(assignment.id, assignment);
  }

  setEmployeeAssignment = (assignment: EmployeeAssignment) => {
    this.employeeAssignments.set(assignment.id, assignment);
  }

  loadAssignments = async (scheduleId: string) => {
    try {
      this.isLoading = true;
      this.clearAssignments();
      const result = await agent.Assignments.list(scheduleId);
      result.data.forEach((assignment: Assignment) => {
        this.setAssignment(assignment);
      });
      this.isLoading = false;
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }

  loadEmployeeAssignments = async () => {
    try {
      const userId = store.userStore.loggedUser?.id;
      this.isLoading = true;
      this.clearEmployeeAssignments();
      const result = await agent.Assignments.employeeList(userId!);
      result.data.forEach((assignment: EmployeeAssignment) => {
        this.setEmployeeAssignment(assignment);
      });
      this.isLoading = false;
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }

  createAssignment = async (assignment: AssignmentFormValues) => {
    try {
      const response = await agent.Assignments.create(assignment);
      const newAssignment = new Assignment(assignment);
      this.setAssignment(newAssignment);

      return response;
    } catch(error) {
      console.log(error);
    }
  }

  deleteAssignment = async (id: string) => {
    this.isLoading = true;
    try {
      await agent.Stops.delete(id);
      runInAction(() => {
        this.selectedAssignments.delete(id);
        this.isLoading = false;
      });
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }
}