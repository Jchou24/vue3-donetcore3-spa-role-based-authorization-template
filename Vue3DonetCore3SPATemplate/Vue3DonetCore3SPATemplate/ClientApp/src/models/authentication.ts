// interface IUserRole{
    
// }

interface IClaims{
    emailaddress: string;
    name: string;
    role: string
}

interface IAuthentication{
    isAuthenticated: boolean;
    // userRoles: Array<IUserRole>;
    claims: IClaims;
}

export {
    // IUserRole,
    IClaims,
    IAuthentication,
}