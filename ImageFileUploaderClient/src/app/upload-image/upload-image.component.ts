import { HttpClient, HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output, Inject } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-upload',
  templateUrl: './upload-image.component.html'
})
export class UploadImageComponent implements OnInit {
  progress: number = 0;
  message: string = '';
  imageUrl: string = '';
  fileName: string = '';
  @Output() public onUploadFinished = new EventEmitter();

  constructor(private http: HttpClient, private sanitizer: DomSanitizer) { }

  ngOnInit() {
  }

  uploadFile = (files: FileList | undefined | null) => {
    if (!files || files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.http.post('https://localhost:7007/imagefileuploader', formData, { reportProgress: true, observe: 'events' })
      .subscribe({
        next: (event) => {
          if (event.type === HttpEventType.UploadProgress && event.total)
            this.progress = Math.round(100 * event.loaded / event.total);
          else if (event.type === HttpEventType.Response) {
            this.message = 'Upload success.';
            this.onUploadFinished.emit(event.body);

            var body = event.body as Blob;

            this.imageUrl = body.uri;
            this.fileName = body.name;
          }
        },
        error: (err: HttpErrorResponse) => {
          this.message = err.error;
        }
      });
  }

}

export interface Blob {
  blobId: number,
  uri: string
  name: string
  storeLocation: string
}
