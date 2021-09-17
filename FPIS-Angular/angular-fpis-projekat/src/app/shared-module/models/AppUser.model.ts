import { AppUserClaim } from "./AppUserClaim.model";

export class AppUser {
    userName: string = "";
    bearerToken: string = "";
    refreshToken: string = "";
    isAuthenticated: boolean = false;
    claims: AppUserClaim[] = [];

    public isEmpty() : boolean{
        return this.userName && this.bearerToken && this.refreshToken && this.isAuthenticated && this.claims.length 
            ? false : true;
    }
}