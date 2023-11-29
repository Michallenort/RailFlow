import { makeAutoObservable, runInAction } from "mobx";
import { Assignment, AssignmentFormValues } from "../models/assignment";
import { Pagination, PagingParams } from "../models/pagination";
import agent from "../api/agent";

export default class AssignmentStore {
  selectedAssignments = new Map<string, Assignment>();
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

  clearStops = () => {
    this.selectedAssignments.clear();
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