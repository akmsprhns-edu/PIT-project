import { BaseResponse } from "./base-response";

export class User implements BaseResponse {
  success: boolean;
  error?: string;
  id: number;
  username: string;
  email:string;
  firstName: string;
  lastName: string;
  token?: string;
}