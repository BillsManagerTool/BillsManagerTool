import { IAuthenticateRequest } from '../interfaces/authenticateRequest';

export class AuthenticateRequest implements IAuthenticateRequest {
  Email: string;
  Password: string;
}
