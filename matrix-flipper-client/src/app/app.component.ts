import { AfterContentInit, Component } from '@angular/core';
import { Matrix } from './models/matrix';
import { Tile, TileState } from './models/tile';
import { MatrixService } from './services/matrix.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterContentInit {
  title = 'flipping-matrix-client';
  currentMatrix: Matrix;
  matrices: Matrix[];
  matrixName: string = '';
  cellWidth: string = '';
  cellHeight: string = '';

  constructor(private matrixService: MatrixService) {
    this.currentMatrix = matrixService.getCurrentMatrix();
    this.matrices = matrixService.getMatrices();

    this.matrixService.currentMatrixChanged.subscribe(() => {
      this.currentMatrix = this.matrixService.getCurrentMatrix();
      this.setCellSize();
    });

    this.matrixService.matricesChanged.subscribe(() => 
      this.matrices = matrixService.getMatrices()
    )
  }

  cellClicked(tile: Tile): void {
    tile.state++;
    if (tile.state > TileState.OK)
      tile.state = TileState.Untouched;
  }

  canSave(): boolean {
    return this.matrixName.length > 0;
  }

  saveButtonClicked(): void {
    this.currentMatrix.name = this.matrixName;
    this.matrixService.saveCurrentMatrix();
    this.matrixName = '';
  }

  activateButtonClicked(name: string): void {
    this.matrixService.activateMatrix(name);
    this.matrixName = this.currentMatrix.name;
  }

  deleteButtonClicked(name: string): void {
    this.matrixService.deleteMatrix(name);
    this.matrixName = '';
  }

  ngAfterContentInit(){
    this.setCellSize();
  }
  
  private setCellSize() {
    this.cellHeight = `calc(100% / ${ this.currentMatrix.rows })`;
    this.cellWidth = `calc(100% / ${ this.currentMatrix.columns })`;
  }
}
