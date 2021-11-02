import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Matrix } from '../models/matrix';
import { TileState } from '../models/tile';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MatrixService {
  matricesChanged: Subject<boolean> = new Subject();
  currentMatrixChanged: Subject<boolean> = new Subject();
  private matrices: Matrix[];
  private currentMatrix: Matrix;
  private apiUrl: string;

  constructor(private httpClient: HttpClient) {
    this.apiUrl = environment.api_url;
    this.matrices = [];
    this.getAllMatricesFromServer();
    this.currentMatrix = this.createEmptyMatrix(6,6);
  }

  getCurrentMatrix(): Matrix {
    return this.currentMatrix;
  }

  getMatrices(): Matrix[] {
    return this.matrices;
  }

  deleteMatrix(name: string): void {
    this.deleteMatrixFromServer(name);
  }

  activateMatrix(name: string): void {
    this.currentMatrix = this.matrices.filter(matrix => matrix.name === name)[0];
    this.currentMatrixChanged.next();
  }

  saveCurrentMatrix(): void {
    this.saveMatrixToServer(this.currentMatrix);
    this.currentMatrix = this.createEmptyMatrix(6,6);
    this.currentMatrixChanged.next();
  }

  private createEmptyMatrix(rows: number, cols: number): Matrix {
    let matrix: Matrix = {
      name: '',
      rows: rows,
      columns: cols,
      tiles: []
    };

    for (let i = 0; i < rows * cols; i++)
      matrix.tiles.push({ state: TileState.Untouched, id: i });

    return matrix;
  }

  // Server calls
  private saveMatrixToServer(matrix: Matrix): void {
    this.httpClient.post(this.apiUrl, matrix).subscribe(() => {
      this.getAllMatricesFromServer();
    })
  }

  private getAllMatricesFromServer(): void {
    this.httpClient.get<Matrix[]>(this.apiUrl).subscribe((data) => {
      this.matrices = data;
      this.matricesChanged.next();
    })
  }

  private deleteMatrixFromServer(name: string): void {
    this.httpClient.delete(this.apiUrl + "/" + name).subscribe(() => {
      if (this.currentMatrix.name === name) {
        this.currentMatrix = this.createEmptyMatrix(6,6);
        this.currentMatrixChanged.next();
      }

      this.getAllMatricesFromServer();
    })
  }
}
