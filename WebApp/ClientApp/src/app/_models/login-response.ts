import { BaseResponse } from "./base-response";

export interface LoginResponse extends BaseResponse{
    id: number;
    username: string;
    firstName: string;
    lastName: string;
    token?: string;
}