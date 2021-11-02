export enum TileState {
    Untouched,
    Error,
    OK
}

export interface Tile {
    state: TileState,
    id: number
}