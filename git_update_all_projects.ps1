$projects = @(
"cv-spider-v2-github",
"cv-spider-v3-github",
"cv-spider-v4-github",
"cv-spider-v5-console-final-github",
"dom-parser-test-github",
"dot-net-spiders-github",
"dynamic-video-github",
"elasticsearch-logstash-kibana-learning-github",
"email-searcher-github",
"email-service-github",
"empty-directories-github",
"es2021-github",
"event-dates-calendar-github",
"event-dates-calendar-ts-github",
"excel-to-json-github",
"files-spell-checker-github",
"flexbox-learning-github",
"flexbox-plays-github",
"forkify-search-over-1-000-000-recipes-github",
"full-text-search-github",
"gmail-detector-github",
"hackathon-task-github",
"hitech-buster-class-msdn-final-course-project-github",
"israeli-group-coaching-github",
"javascript-learning-github",
"job-interview-exercises-2023-github",
"job-interview-exercises-github",
"job-interview-sender-github",
"material-ui-admin-github",
"mbox-crawler-github",
"movies-library-github",
"nextjs-blog-github",
"nextjs-example-github",
"node-microservice-boilerplate-github",
"node-red-blog-github",
"node-test-restart-github",
"node-vidly-deployment-github",
"node-vidlys-github",
"nodejs-layered-architecture-github",
"nodejs-learning-v1-github",
"nodejs-learning-v2-github",
"papaito-github",
"pizza-restaurant-github",
"puppeteer-example-github",
"rabbit-mq-learning-github",
"react-bootstrap-4-github",
"react-learning-v1-github",
"react-learning-v2-github",
"renamer-github",
"sender-github",
"simple-form-github",
"snowflake-github",
"solutions-v1-github",
"solutions-v2-github",
"spam-revenge-github",
"stackoverbot-github",
"starter-kits-2023-github",
"starter-kits-github",
"stream-images-github",
"styled-budgety-calculator-github",
"temperature-city-displayer-github",
"top-packages-github",
"trello2.0-github",
"typescript-learning-github",
"typescript-user-authentication-server-github",
"udemy-courses-github",
"users-list-github",
"users-manager-github",
"world-covid-19-data-cra-github",
"world-covid-19-data-nextjs-github",
"youtube-comments-github"
)

$baseDir = "C:\Or\web\projects"

foreach ($project in $projects) {
    $dir = Join-Path $baseDir $project
    if (Test-Path $dir) {
        Write-Host ">>> Processing: $project"
        Set-Location $dir
        git add .
        git commit -m "Update the MD files, license and package.json, Add GitHub Rulesets"
        git push --force-with-lease
        Start-Sleep -Seconds 1
    } else {
        Write-Host ">>> Directory not found: $dir"
    }
}
