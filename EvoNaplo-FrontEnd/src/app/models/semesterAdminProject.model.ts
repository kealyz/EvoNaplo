import { SemesterAdminUserModel } from "src/app/models/semesterAdminUser.model";

export interface SemesterAdminProjectModel {
    id : number;
    name: string;
    students: SemesterAdminUserModel[];
}