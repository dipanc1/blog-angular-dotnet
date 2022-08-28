import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/models/post.model';
import { UpdatePost } from 'src/app/models/update-post.model';
import { PostsService } from 'src/app/services/posts.service';

@Component({
  selector: 'app-admin-view-post',
  templateUrl: './admin-view-post.component.html',
  styleUrls: ['./admin-view-post.component.css']
})
export class AdminViewPostComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private postService: PostsService,
    private router: Router
  ) { }

  post: Post | undefined;

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      params => {
        const id = params.get('id')

        if (id) {
          this.postService.getPostById(id).subscribe(
            response => (
              this.post = response
              // console.log(response)
            )
          )
        }
      }
    )
  }

  onSubmit(): void {
    const updatePostRequest: UpdatePost = {
      author: this.post?.author,
      content: this.post?.content,
      featuredImage: this.post?.featuredImage,
      publishDate: this.post?.publishDate,
      updateDate: this.post?.updateDate,
      visible: this.post?.visible,
      summary: this.post?.summary,
      title: this.post?.title,
      urlHandle: this.post?.urlHandle
    }

    this.postService.updatePost(this.post?.id, updatePostRequest).subscribe(
      res => (
        alert("CHAL PYA BHE CHODDAAA")
      )
    )
  }

  deletePost(): void {
    this.postService.deletePost(this.post?.id).subscribe(
      res => {
        this.router.navigate(['/admin/posts'])
      }
    )
  }

}
