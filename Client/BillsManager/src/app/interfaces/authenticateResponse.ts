import { IBaseResponse } from './baseResponse';

export interface IAuthenticateResponse extends IBaseResponse {
  JWT: string;
  email: string;
}
