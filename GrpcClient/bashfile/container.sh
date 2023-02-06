docker run -itd --rm --privileged --name $2 -v $PWD/executeFiles/$2/data:/opt/data -v $PWD/executeFiles/$2/bin:/opt/bin/ $1
