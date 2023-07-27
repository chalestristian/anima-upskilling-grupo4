import { GRANT_TYPE } from '@/tokenacess';
import { CLIENT_ID } from '@/tokenacess';
import { CLIENT_SECRET } from '@/tokenacess';
import { SCOPE } from '@/tokenacess';

export class AuthModel{
    grant_type = GRANT_TYPE.toString();
    client_id = CLIENT_ID.toString();
    client_secret = CLIENT_SECRET.toString();
    scope = SCOPE.toString();
}