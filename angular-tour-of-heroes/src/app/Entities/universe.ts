import { ListItem } from 'src/app/Entities/listitem';

export class Universe {
    universeId: number;
    universeName: string;
    logoUrl: string;
    heroes: ListItem[];
}