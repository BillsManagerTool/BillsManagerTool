import { IBaseResponse } from './../interfaces/baseResponse';
export class AuthenticateResponse implements IBaseResponse {
  JWT: string;
  email: string;
  StatusCode: number;
}
