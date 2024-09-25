import { Course } from "./Course";

export interface Student {
    Id: number;
    firstName: string;
    lastName: string;
    age: number;
    dateOfBirth: Date;
    address: string;
    courseId: number;
    course?: Course;
  }