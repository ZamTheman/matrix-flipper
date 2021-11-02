import { Tile } from "./tile";

export interface Matrix {
    name: string;
    rows: number;
    columns: number;
    tiles: Tile[];
}