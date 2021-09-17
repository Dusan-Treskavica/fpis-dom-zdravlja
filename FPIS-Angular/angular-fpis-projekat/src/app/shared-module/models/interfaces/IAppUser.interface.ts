import { AppUserClaim } from "../AppUserClaim.model";

export interface IAppUser{
    userName: string;
    bearerToken: string;
    isAuthenticated: boolean;
    claims: AppUserClaim[];

    isEmpty: () => boolean;
}